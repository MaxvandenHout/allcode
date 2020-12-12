import { Store } from "./state";

export class List<T> {
    state = new Store<Array<T>, T>();

    get obsValue() {
        return this.state.obsValue;
    }

    get value() {
        return this.state.value;
    }

    change(newData: Array<T>) {
        this.state.change(newData);
    }

    delete(){
        this.state.delete();
    }

    push(newData: T){
        this.state.push(newData)
    }

    update(func: Function, newData: T){
        this.state.update(func, newData)
    }
}