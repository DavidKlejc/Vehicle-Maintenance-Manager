export interface IOwner {
  id: string;
  firstName: string;
  lastName: string;
  createdAt: string;
}

export interface ICreateOwnerDto {
  firstName: string;
  lastName: string;
}

export interface IVehicle {
  id: string;
  vin: string;
  make: string;
  model: string;
  year: string;
  serviceRecords: string;
  ownerName: string;
}

export interface ICreateVehicleDto {
  ownerId: string;
  vin: string;
  make: string;
  model: string;
  year: string;
  serviceRecords: string;
}
