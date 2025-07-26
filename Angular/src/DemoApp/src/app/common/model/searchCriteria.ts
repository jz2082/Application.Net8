import { SearchRule } from './searchRule';

export class SearchCriteria {
    index: number | undefined;
    pageSize: number | undefined;
    count: number | undefined;
    searchRuleList: SearchRule[];
    sortList: [];

    constructor() {
        this.searchRuleList = [];
        this.sortList = [];
    }
}
