import { MyDto } from "./dto.generated";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ConfigService } from "@mirage/config";
import { WebClient } from "@mirage/webclient";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class NullableReturnTypeTestService extends WebClient {
	get endPoint(): string  {
		return this.config.endpoint("test-client") + "api/nullable-return-type";
	}
	constructor(private config: ConfigService, protected client: HttpClient) {
		super();
		console.log("NullableReturnTypeTestService instance created");
	}
	getString(text: string): Observable<string>  {
		const relativeUrl = `string`;
		const result = this.doGetAsync<string>(relativeUrl, { text });
		return result;
	}
	getAsyncString(text: string): Observable<string>  {
		const relativeUrl = `async-string`;
		const result = this.doGetAsync<string>(relativeUrl, { text });
		return result;
	}
	getActionResultString(text: string): Observable<string>  {
		const relativeUrl = `action-result-string`;
		const result = this.doGetAsync<string>(relativeUrl, { text });
		return result;
	}
	getAsyncActionResultString(text: string): Observable<string>  {
		const relativeUrl = `async-action-result-string`;
		const result = this.doGetAsync<string>(relativeUrl, { text });
		return result;
	}
	getInt(n: number|undefined): Observable<number|undefined>  {
		const relativeUrl = `int`;
		const result = this.doGetAsync<number|undefined>(relativeUrl, { n });
		return result;
	}
	getAsyncInt(n: number|undefined): Observable<number|undefined>  {
		const relativeUrl = `async-int`;
		const result = this.doGetAsync<number|undefined>(relativeUrl, { n });
		return result;
	}
	getActionResultInt(n: number|undefined): Observable<number|undefined>  {
		const relativeUrl = `action-result-int`;
		const result = this.doGetAsync<number|undefined>(relativeUrl, { n });
		return result;
	}
	getAsyncActionResultInt(n: number|undefined): Observable<number|undefined>  {
		const relativeUrl = `async-action-result-int`;
		const result = this.doGetAsync<number|undefined>(relativeUrl, { n });
		return result;
	}
	getDateTime(v: Date|undefined): Observable<Date|undefined>  {
		const relativeUrl = `datetime`;
		const result = this.doGetAsync<Date|undefined>(relativeUrl, { v });
		return result;
	}
	getAsyncDateTime(v: Date|undefined): Observable<Date|undefined>  {
		const relativeUrl = `async-datetime`;
		const result = this.doGetAsync<Date|undefined>(relativeUrl, { v });
		return result;
	}
	getActionResultDateTime(v: Date|undefined): Observable<Date|undefined>  {
		const relativeUrl = `action-result-datetime`;
		const result = this.doGetAsync<Date|undefined>(relativeUrl, { v });
		return result;
	}
	getAsyncActionResultDateTime(v: Date|undefined): Observable<Date|undefined>  {
		const relativeUrl = `async-action-result-datetime`;
		const result = this.doGetAsync<Date|undefined>(relativeUrl, { v });
		return result;
	}
	getMyDto(value: MyDto|undefined): Observable<MyDto|undefined>  {
		const relativeUrl = `object`;
		const result = this.doPostAsync<MyDto|undefined, MyDto|undefined>(relativeUrl, value, {});
		return result;
	}
	getAsyncMyDto(value: MyDto|undefined): Observable<MyDto|undefined>  {
		const relativeUrl = `async-object`;
		const result = this.doPostAsync<MyDto|undefined, MyDto|undefined>(relativeUrl, value, {});
		return result;
	}
	actionResultObject(value: MyDto|undefined): Observable<MyDto|undefined>  {
		const relativeUrl = `action-result-object`;
		const result = this.doPostAsync<MyDto|undefined, MyDto|undefined>(relativeUrl, value, {});
		return result;
	}
	asyncActionResultObject(value: MyDto|undefined): Observable<MyDto|undefined>  {
		const relativeUrl = `async-action-result-object`;
		const result = this.doPostAsync<MyDto|undefined, MyDto|undefined>(relativeUrl, value, {});
		return result;
	}
	getMyDtoNullableArray(values: (MyDto|undefined)[]): Observable<(MyDto|undefined)[]>  {
		const relativeUrl = `nullable-array-return-type`;
		const result = this.doPostAsync<(MyDto|undefined)[], (MyDto|undefined)[]>(relativeUrl, values, {});
		return result;
	}
	getMyDtoCollection(values: (MyDto|undefined)[]): Observable<(MyDto|undefined)[]>  {
		const relativeUrl = `nullable-collection-return-type`;
		const result = this.doPostAsync<(MyDto|undefined)[], (MyDto|undefined)[]>(relativeUrl, values, {});
		return result;
	}
}