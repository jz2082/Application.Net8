export class BaseEntity {
  id: string | undefined;
  dateCreated: Date | undefined;
  dateModified: Date | undefined;
  rowVersion: any[] | undefined;
}
