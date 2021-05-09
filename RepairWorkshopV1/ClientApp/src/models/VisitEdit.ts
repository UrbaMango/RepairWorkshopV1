export interface VisitEdit {
  visitId: number;
  clientId: number;
  vehicleId: number;
  mileage: number;
  visitStartDate: Date;
  visitEndDate: Date;
  notes: string;
  visitPrice: number;
  confirmed: boolean;
  progress: string;
}
