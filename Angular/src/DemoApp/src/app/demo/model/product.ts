import { BaseEntity } from '@common/model/baseEntity';

export class Product extends BaseEntity {
  productId: number;
  productName: string;
  productCode: string;
  tagList?: string[];
  releaseDate: Date;
  price: number;
  description: string;
  starRating: number;
  imageUrl: string;
}
