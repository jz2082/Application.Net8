export class Dictionary {
    value: { [key: string]: string } = {};

    constructor() {
        this.value = {};
    }

    public hasKey(key: string): boolean {
        return key in this.value;
    }

    public setValue(key: string, value: string) {
        if (this.hasKey(key)) {
            this.value[key] = this.value[key] + "<br />" + value;
        }
        else {
            this.value[key] = value;
        }
    }

    public getValue(key: string): string {
        let returnValue = '';
        if (this.hasKey(key)) {
            returnValue =  this.value[key];
        }
        return returnValue;
    }

    public delete(key: string) {
        if (this.hasKey(key)) {
            delete this.value[key];
        }
    }
}
