import { List } from "../../../../util/lists";
import { Store } from "../../../../util/state";
import { TestModel } from "./Models/testmodel";

export class TestService{
    onestate: Store<TestModel> = new Store<TestModel>();
    listState: List<TestModel> = new List<TestModel>();

    storeModel(test: TestModel){
        this.onestate.change(test);
    }

    get value(){
        return this.onestate.value
    }

    get obsValue(){
        return this.onestate.obsValue
    }

    addStored(){
        this.listState.push(this.onestate.value);
    }

    addNew(test: TestModel){
        this.listState.push(test);
    }

    resetState(){
        this.listState.delete();
        this.listState.delete();
    }

    toggle(){
        this.onestate.update((st) => {
            st.completed = !st.completed;
            return st
        });
    }
}