from manager import ServiceManager

if __name__ == '__main__':
    service_name = 'VehicleRegistrationReaderService'

    port_number = 61408
    port_number_ssl = 5002

    default_http = input("Use default port number for http (61408) [yes/no]: ")
    if default_http.strip().lower() != 'yes':
        while True:
            try:
                port_number = int(input("Enter port number: "))
                break
            except Exception as e:
                print('Please enter valid number')
    
    default_https = input("Use default port number for https (5002) [yes/no]: ")
    if default_https.strip().lower() != 'yes':
        while True:
            try:
                port_number_ssl = int(input("Enter port number for ssl: "))
                if port_number_ssl == port_number:
                    print('Port for http and https can\'t be same, please select different port')
                else:
                    break
            except Exception as e:
                print('Please enter valid number')

    installer = ServiceManager(service_name, port_number, port_number_ssl)
    installer.create_service()