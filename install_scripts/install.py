from manager import ServiceManager

if __name__ == '__main__':
    service_name = 'VehicleRegistrationReaderService'

    port_number = 61408

    default = input("Use default port number (61408) [yes/no]: ")
    if default.strip().lower() != 'yes':
        while True:
            try:
                port_number = int(input("Enter port number: "))
                break
            except Exception as e:
                print('Please enter valid number')

    installer = ServiceManager(service_name, port_number)
    installer.create_service()