﻿using AutoRental.Client.Entities;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AutoRental.Client.Contracts
{
   [ServiceContract]
   public interface IInventoryService : IServiceContract
   {
      [OperationContract]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      Car UpdateCar(Car car);

      [OperationContract]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      void DeleteCar(int carId);

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      Car GetCar(int carId);

      [OperationContract]
      Car[] GetAllCars();

      [OperationContract]
      Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate);

      //
      #region Async operations

      [OperationContract]
      Task<Car> UpdateCarAsync(Car car);

      [OperationContract]
      Task DeleteCarAsync(int carId);

      [OperationContract]
      Task<Car> GetCarAsync(int carId);

      [OperationContract]
      Task<Car[]> GetAllCarsAsync();

      [OperationContract]
      Task<Car[]> GetAvailableCarsAsync(DateTime pickupDate, DateTime returnDate);

      #endregion
   }
}
