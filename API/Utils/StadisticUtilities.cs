using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace CarDepo.API.Utils;

public class StadisticUtilities
{
    public readonly AuthUtilities _authutils;
    public readonly ICarRepository _carrepo;
    public readonly IMakeRepository _makerepo;
    public readonly IDriverRepository _driverrepo;
    public readonly ICarDriverRepository _cardriverrepo;

    public StadisticUtilities(AuthUtilities utilities, ICarRepository carrepo, IMakeRepository makerepo, IDriverRepository driverepo, ICarDriverRepository cardriverrepo)
    {
        _authutils = utilities;
        _carrepo = carrepo;
        _makerepo = makerepo;
        _driverrepo = driverepo;
        _cardriverrepo = cardriverrepo;
    }

        public async Task<Double> GetCarKms()
        {
            IEnumerable<Car> cars = await _carrepo.GetCars();
            return cars.Average(c => c.Kms);
        }

        public async Task<IEnumerable> GetCarColorCount()
        {
            IEnumerable<Car> cars = await _carrepo.GetCars();
            return cars.CountBy(c => c.ColorId);
        }

        public async Task<IEnumerable> GetCarMakeCount()
        {
            IEnumerable<Car> cars = await _carrepo.GetCars();
            return cars.CountBy(c => c.MakeId);
        }

        public async Task<IEnumerable> GetCarOwnerCount()
        {
            IEnumerable<Car> cars = await _carrepo.GetCars();
            return cars.CountBy(c => c.OwnerId);
        }

        public async Task<IEnumerable> GetCarDriverAssociatedCarCount()
        {
            IEnumerable<CarDriver> drivers = await _cardriverrepo.GetCarDrivers();
            return drivers.CountBy(d => d.CarCDId);
        }

        public async Task<Decimal> GetAveragePrice()
        {
            IEnumerable<Make> makes = await _makerepo.GetMakes();
            return makes.Average(m => m.Price);
        }
}