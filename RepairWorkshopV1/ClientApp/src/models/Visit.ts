export interface Visit {
  visitId: number;
  clientId: number;
  vehicleId: number;
  mileage: number;
  visitStartDate: Date;
  visitEndDate: Date;
  notes: string;
  visitPrice: null;
  confirmed: true;
  progress: string;
  vehicle: null;
  visitTasks: [];
}
