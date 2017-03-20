﻿using AutoRental.Business.Entities;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRental.Data.Contracts
{
   public interface IRentalRepository : IDataRepository<Rental>
   {
      IEnumerable<Rental> GetRentalHistoryByCar(int carId);
      Rental GetCurrentRentalByCar(int carId);
      IEnumerable<Rental> GetCurrentlyRentedCars();
      IEnumerable<Rental> GetRentalHistoryByAccount(int accountId);
      IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo();
      IEnumerable<CustomerRentalInfo> GetCustomerRentalInfoByAccount(int accountId);
   }
}