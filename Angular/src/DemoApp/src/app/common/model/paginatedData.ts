import { AdditionalInfo } from './additionalInfo';

export class PaginatedData<T> {
    index: number | undefined;
    pageIndex: number | undefined;
    pageSize: number | undefined;
    hasFirstLastPage: boolean | undefined;
    hasPreviousPage: boolean | undefined;
    hasNextPage: boolean | undefined;
    pageCount: number | undefined;
    count: number | undefined;
    data: T | undefined;
}
