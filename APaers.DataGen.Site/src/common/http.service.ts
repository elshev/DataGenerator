import {Injectable} from "@angular/core";
import {HttpClient, HttpParams, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs/Observable";

import {environment} from "../environments/environment";

@Injectable()
export class HttpService {
    constructor(private httpClient: HttpClient) {

    }

    getUrl(route: string): string {
        return environment.apiUrl + route;
    }

    get<T>(route: string, params: HttpParams): Observable<T> {
        const url = this.getUrl(route);
        return this.httpClient.get<T>(url, { params });
    }

    post<T>(route: string, data: any): Observable<T> {
        const url = this.getUrl(route);
        const headers = new HttpHeaders({"Content-Type": "application/json"});
        return this.httpClient.post<T>(url, JSON.stringify(data), {headers: headers});
    }

    /*For common method see: https://stackoverflow.com/questions/37051476/how-do-i-post-json-in-angular-2
    http(method: any, route: string, params: any, data: any, successFunction: any, errorFunction: any): void {
        const url = this.getUrl(route);
        this.httpClient.request(method, url,  )
        $http({
                url: url,
                method: method,
                params: params,
                data: data
            })
            .then(
                function(response) {
                    if (successFunction)
                        return successFunction(response.data);
                },
                function(response) {
                    if (errorFunction)
                        return errorFunction(response);
                });
    }*/

    /*postParams(route, params, successFunction, errorFunction) {
        http("POST", route, params, null, successFunction, errorFunction);
    }

    put(route, params, data, successFunction, errorFunction) {
        http("PUT", route, params, data, successFunction, errorFunction);
    }

    remove(route, params, successFunction, errorFunction) {
        http("DELETE", route, params, null, successFunction, errorFunction);
    }*/
}
