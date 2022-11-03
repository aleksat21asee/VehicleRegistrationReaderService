from subprocess import PIPE, run, CompletedProcess
import sys
import os
from enum import Enum

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
    def __init__(self, service_name: str) -> None:
         self.root_path = os.getcwd()
         self.service_name = service_name

    # install & run service
    def create_service(self):
        # Check if service is installed
        if self._service_exists():
            self._terminate(f'Service {self.service_name} is already installed!')

        executor = CommandExecutor()

        # install service
        cmd = f'New-Service {self.service_name} -BinaryPathName "{self.root_path}\\{self.service_name}.exe"'
        action_status, completed_process = executor.execute_command(cmd, f'Creating service {self.service_name} ...')
        self._check_terminate(action_status, completed_process)

        # run service
        cmd = f'Set-Service -Name {self.service_name} -Status Running'
        action_status, completed_process = executor.execute_command(cmd, f'Running service {self.service_name} ...')
        self._check_terminate(action_status, completed_process)

        self._terminate(f'Service {self.service_name} has been succesfully installed!')


    # uninstall service
    def uninstall_service(self):
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


        self._terminate(f'Service {self.service_name} has been uninstalled! You may need to restart your computer for this action to take effect.')

        

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
