import { PlatformLocation } from "@angular/common";
import { Injectable } from "@angular/core";

import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Rx";

import { IUpdateViewModel } from "../interfaces/index";
import { ServiceHelpers } from "./service.helpers";

@Injectable()
export class UpdateService extends ServiceHelpers {
    constructor(http: HttpClient, public platformLocation: PlatformLocation) {
        super(http, "/api/v1/update/", platformLocation);
    }
    public getUpdate(): Observable<IUpdateViewModel> {
        return this.http.get<IUpdateViewModel>(`${this.url}/`, {headers: this.headers});
    }
}
