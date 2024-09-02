using Models;

namespace Service;

public interface IDevicesService
{
    List<Device> GetDevices();
    Device GetDeviceById(int id);
    bool CreateDevice(Device device);
    bool UpdateDevice(Device device);
    bool DeleteDevice(int id);
}
