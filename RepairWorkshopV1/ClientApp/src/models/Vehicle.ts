export interface Vehicle {
  vehicleId: number;
  make: string;
  vin: string;
  model: string;
  year: string;
  licensePlate: string;
  clientId: number;
  client: null;
  visits: []
}
