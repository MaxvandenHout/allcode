import { BehaviorSubject, Observable, Subject } from "rxjs";

export class Store<T, F = T> {

    private _value: BehaviorSubject<T> = new BehaviorSubject(null);

    constructor() {
    }

    get obsValue() {
        return this.asObservable(this._value);
    }

    get value() {
        return this._value.getValue();
    }

    change(newData: T) {
        this._value.next(newData);
    }

    delete(){
        this._value.next(null);
    }

    push(newData: F){
        const value = this._value.getValue();
        if (Array.isArray(value)) {
            let array: any;
            array = value.push(newData);
            this._value.next(array);
        }
    }

    update(func: Function, newData: F = null){
        const value = this._value.getValue();
        const newVersion = func(value, newData)
        this._value.next(newVersion);
    }

    private asObservable(subject: BehaviorSubject<T>) {
        return new Observable(fn => subject.subscribe(fn));
    };

}