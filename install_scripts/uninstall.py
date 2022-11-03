from manager import ServiceManager

if __name__ == '__main__':
    service_name = 'VehicleRegistrationReaderService'

    installer = ServiceManager(service_name)
    installer.uninstall_service()