import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ConfigService } from "@my_module/config";
import { WebClient } from "@my_module/webclient";
import { format } from "date-fns";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class ArrayParamTestService extends WebClient {
	get endPoint(): string  {
		return this.config.endpoint("endpoint_name") + "api/array-param-test";
	}
	constructor(private config: ConfigService, protected client: HttpClient) {
		super();
		console.log("ArrayParamTestService instance created");
	}
	arrayStringParam(array: string[]): Observable<string>  {
		const relativeUrl = `array-string-param`;
		const result = this.doGetStringAsync(relativeUrl, { a: array });
		return result;
	}
	arrayValueType(array: number[]): Observable<string>  {
		const relativeUrl = `array-value-type`;
		const result = this.doGetStringAsync(relativeUrl, { a: array });
		return result;
	}
	collectionStringParam(collection: string[]): Observable<string>  {
		const relativeUrl = `collection-string-param`;
		const result = this.doGetStringAsync(relativeUrl, { c: collection });
		return result;
	}
	collectionValueType(collection: number[]): Observable<string>  {
		const relativeUrl = `collection-value-type`;
		const result = this.doGetStringAsync(relativeUrl, { c: collection });
		return result;
	}
	collectionDateParam(collection: Date[]): Observable<string>  {
		const relativeUrl = `collection-date-param`;
		const result = this.doGetStringAsync(relativeUrl, { c: collection.map(x => format(x, "yyyy-MM-dd")) });
		return result;
	}
	collectionDateTimeParam(collection: Date[]): Observable<string>  {
		const relativeUrl = `collection-datetime-param`;
		const result = this.doGetStringAsync(relativeUrl, { c: collection.map(x => format(x, "yyyy-MM-ddTHH:mm:ssXXX")) });
		return result;
	}
}
