import { Component, OnInit } from "@angular/core";
import axios from 'axios';
import { environment } from '../environments/environment';



@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.css"],
    providers: []
})
export class AppComponent {
    username = '';
    text = '';
    messages = [];
    typeusername: '';
   
    login() {
        this.username = this.typeusername;
    }

    ngOnInit() {
        //When database exists a call would be made here from the initial messages from the database.
               
    }

    constructor() {
        var app = this;
        var socket = new WebSocket(environment.CHAT_URL);
        socket.onopen = function () {   
        };
        socket.onmessage = function (evt) {
            let data = JSON.parse(evt.data);
            console.log(data);
            app.messages.push(data);
            return false;
        };
        socket.onclose = function () {
        };
    }


    sendMsg() {
        axios.post(environment.POSTGET_URL, {
            Username: this.username,
            Text: this.text
        }).then(() => {
            this.text = "";
        })
        
    }
}
