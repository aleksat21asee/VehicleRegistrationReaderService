from subprocess import PIPE, run, CompletedProcess
import sys
import os
from enum import Enum
import re

class ActionStatus(Enum):
    OK = 0,
    ERROR_ABORT = 2

class CommandExecutor:
        def __init__(self) -> None:
             pass
        
        def execute_command(self, cmd: str, msg: str):
            print(msg)
            status = run(["powershell", "-Command", cmd], stdout=PIPE, stderr=PIPE, universal_newlines=True)
            return self._handle_status(status)    
            
        def _handle_status(self, completed_process: CompletedProcess):
            if completed_process.returncode != 0:
                return ActionStatus.ERROR_ABORT, completed_process
            else:
                return ActionStatus.OK, completed_process
            

class ServiceManager: 
    def __init__(self, service_name: str, port_number: int = 61408, port_number_ssl = 5002) -> None:
        self.root_path = os.getcwd()
        self.service_name = service_name
        self.port_number = port_number
        self.port_number_ssl = port_number_ssl

        self.cert_host = 'vrcardreader.asseco-see.rs'
        self.cert_name = 'vrcardreader.pfx'
        self.cert_password = 'rs.asseco-see.vrcardreader'

        self.cert_path = os.path.join(self.root_path, self.cert_name)

    def handle_certificates(self):
        hosts_path = os.path.join(os.environ['WINDIR'], 'system32', 'Drivers', 'etc', 'hosts')

        try:
            with open(hosts_path, 'a+') as f:
                f.seek(0)
                
                lines = f.readlines()
                data = "\n".join(lines)
                
                ip_adress = '127.0.0.1'
                pattern = rf'.*{re.escape(ip_adress)}\s+{re.escape(self.cert_host)}.*'

                result = re.search(pattern, data)
                if result != None:
                    print('Entry already set')
                else:
                    write_data = f'\n{ip_adress} {self.cert_host}\n'
                    f.write(write_data) 
        except Exception as e:
            self._terminate(f'Error while loading file, {e}')

        executor = CommandExecutor()
        
        # If the certificate already exists, remove it
        cmd = f'''
            $selfSignedCerts = Get-ChildItem -Path cert:\localMachine\my\ | Where-Object {{ $_.subject -eq "CN={self.cert_host}"}}
            \n If($selfSignedCerts) {{
                $selfSignedCerts | Remove-Item
            }}
            \n 
        '''
        action_status, completed_process = executor.execute_command(cmd, f'Removing old certificates...')
        self._check_terminate(action_status, completed_process)
        
        # Generate self signed certificate
        cmd = f'New-SelfSignedCertificate -DnsName {self.cert_host} -CertStoreLocation "cert:\LocalMachine\My"'
        action_status, completed_process = executor.execute_command(cmd, f'Generating certificate...')
        self._check_terminate(action_status, completed_process)

        # export to pfx and import
        cmd = f'''
        \n $pwd = ConvertTo-SecureString -String "{self.cert_password}" -Force -AsPlainText
        \n Get-ChildItem -Path cert:\localMachine\my\ | Where-Object {{ $_.subject -eq "CN={self.cert_host}"}} | Export-PfxCertificate -FilePath "{self.cert_path}" -Password $pwd
        \n Import-PfxCertificate -FilePath "{self.cert_path}" -CertStoreLocation cert:\LocalMachine\Root -Password $pwd
        \n
        '''
        action_status, completed_process = executor.execute_command(cmd, f'Loading certficate...')
        self._check_terminate(action_status, completed_process)


    # install & run service
    def create_service(self):
        self.handle_certificates()

        # Check if service is installed
        if self._service_exists():
            print(f'Service {self.service_name} is already installed!')
            print(f'Removing previous version of {self.service_name}...')

            self.uninstall_service(False)

        executor = CommandExecutor()

        # install service
        cmd = f'''New-Service {self.service_name} -BinaryPathName "{self.root_path}\\{self.service_name}.exe --port-number {self.port_number}
        --cert-host {self.cert_host} --cert-path {self.cert_path} --cert-password {self.cert_password} --port-number-ssl {self.port_number_ssl}"'''
        
        action_status, completed_process = executor.execute_command(cmd, f'Creating service {self.service_name} ...')
        self._check_terminate(action_status, completed_process)

        # run service
        cmd = f'Set-Service -Name {self.service_name} -Status Running'
        action_status, completed_process = executor.execute_command(cmd, f'Running service {self.service_name} ...')
        self._check_terminate(action_status, completed_process)

        self._terminate(f'Service {self.service_name} has been succesfully installed!')


    # uninstall service
    def uninstall_service(self, stop=True):
        # Check if service is not installed
        if not self._service_exists():
            self._terminate(f'{self.service_name} is not installed!')
        
        executor = CommandExecutor()
        
        cmd = f'Set-Service -Name {self.service_name} -Status Stopped'
        action_status, completed_process = executor.execute_command(cmd, f'Stopping service {self.service_name} ...')
        self._check_terminate(action_status, completed_process)

        cmd = f'sc.exe delete {self.service_name}'
        action_status, completed_process = executor.execute_command(cmd, f'Uninstalling service {self.service_name} ...')
        self._check_terminate(action_status, completed_process)

        if stop:
            self._terminate(f'Service {self.service_name} has been uninstalled! You may need to restart your computer for this action to take effect.')
        else:
            print(f'Service {self.service_name} has been uninstalled!')
        
    def _check_terminate(self, action_status: ActionStatus, completed_process: CompletedProcess):
        if action_status == ActionStatus.ERROR_ABORT:
            self._terminate(f'{completed_process.stderr}')
        elif action_status == ActionStatus.OK:
            print(f'{completed_process.stdout}')

    def _terminate(self, msg: str):
        sys.stderr.write(f'{msg}\n')
        input('Press enter to exit..\n')
        exit()
              
    def _service_exists(self) -> bool:
        executor = CommandExecutor()

        cmd = f'Get-Service -Name {self.service_name} -ErrorAction SilentlyContinue'
        action_status, completed_process = executor.execute_command(cmd, '')

        return action_status == ActionStatus.OK
