
import { Injectable } from '@angular/core';
import { Observable, Subject } from "rxjs";
import { map } from 'rxjs/operators';
import { WebsocketService } from "./websocket.service";
import { environment } from "../environments/environment";



export interface Message {
    username: string;
    text: string;
    datetime: string;
    
}

@Injectable()
export class ChatService {
    public messages: Subject<Message>;

    constructor(wsService: WebsocketService) {
        wsService.connect(environment.CHAT_URL);
        //this.messages = <Subject<Message>>wsService.connect(environment.CHAT_URL).subscribe().pipe(map(
        //    (response: MessageEvent): Message => {
        //        console.log("incoming message " + response.data)
        //        return;
        //        //let data = JSON.parse(response.data);
        //        //return {
        //        //    username: data.Username,
        //        //    text: data.Text,
        //        //    datetime: data.DateTime
        //        //};
        //    }
        //));
    }
}
