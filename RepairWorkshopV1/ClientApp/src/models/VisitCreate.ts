export interface VisitCreate {
  clientId: number;
  vehicleId: number;
  mileage: number;
  visitStartDate: Date;
  notes: string;
  confirmed: true;
  progress: string;
}
