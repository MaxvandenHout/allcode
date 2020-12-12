import {Component, Input, Output, EventEmitter, Inject} from 'angular2/core';
import {Todo} from "./Todo";
import {List} from 'immutable';
import {Observer} from "rxjs/Observer";
import {Observable} from "rxjs/Observable";
import {TodoStore} from "./state/TodoStore";


@Component({
    selector: 'todo-list',
    template: `

        <section id="main">
            <label for="toggle-all">Mark all as complete</label>
            <ul id="todo-list">
                <li *ngFor="#todo of todos | async" [ngClass]="{completed: todo.completed}">
                    <div class="view">
                        <input type="button" (click)="onToggleTodo(todo)" [checked]="todo.completed">
                        <label>{{todo.description}}</label>
                        <button class="destroy" (click)="delete(todo)"></button>
                    </div>
                </li>
            </ul>
        </section>
    `
})
export class TodoList {
    todos = this.todoStore.todos;

    constructor(private todoStore: TodoStore) {
        console.log('klm')

    }


    onToggleTodo(todo: Todo) {
        console.log('toogle');
        const todo2 = new Todo({
            id: todo.id,
            description: todo.description,
            completed: !todo.completed
        })
        todo.completed = true;
        this.todoStore.update(todo2);
    }

    delete(todo:Todo) {
        this.todoStore.deleteTodo(todo);
    }

}