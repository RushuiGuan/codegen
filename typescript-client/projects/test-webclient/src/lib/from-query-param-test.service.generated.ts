import { MyEnum } from "./dto.generated";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ConfigService } from "@mirage/config";
import { WebClient } from "@mirage/webclient";
import { format } from "date-fns";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class FromQueryParamTestService extends WebClient {
	get endPoint(): string  {
		return this.config.endpoint("test-client") + "api/from-query-param-test";
	}
	constructor(private config: ConfigService, protected client: HttpClient) {
		super();
		console.log("FromQueryParamTestService instance created");
	}
	requiredString(name: string): Observable<object>  {
		const relativeUrl = `required-string`;
		const result = this.doGetAsync<object>(relativeUrl, { name });
		return result;
	}
	requiredStringImplied(name: string): Observable<object>  {
		const relativeUrl = `required-string-implied`;
		const result = this.doGetAsync<object>(relativeUrl, { name });
		return result;
	}
	requiredStringDiffName(name: string): Observable<object>  {
		const relativeUrl = `required-string-diff-name`;
		const result = this.doGetAsync<object>(relativeUrl, { n: name });
		return result;
	}
	requiredInt(value: number): Observable<object>  {
		const relativeUrl = `required-int`;
		const result = this.doGetAsync<object>(relativeUrl, { value });
		return result;
	}
	requiredDateTime(datetime: Date): Observable<object>  {
		const relativeUrl = `required-datetime`;
		const result = this.doGetAsync<object>(relativeUrl, { datetime: format(datetime, "yyyy-MM-ddTHH:mm:ssXXX") });
		return result;
	}
	requiredDateTimeDiffName(datetime: Date): Observable<object>  {
		const relativeUrl = `required-datetime_diff-name`;
		const result = this.doGetAsync<object>(relativeUrl, { d: format(datetime, "yyyy-MM-ddTHH:mm:ssXXX") });
		return result;
	}
	requiredDateOnly(dateonly: Date): Observable<object>  {
		const relativeUrl = `required-dateonly`;
		const result = this.doGetAsync<object>(relativeUrl, { dateonly: format(dateonly, "yyyy-MM-dd") });
		return result;
	}
	requiredDateOnlyDiffName(dateonly: Date): Observable<object>  {
		const relativeUrl = `required-dateonly_diff-name`;
		const result = this.doGetAsync<object>(relativeUrl, { d: format(dateonly, "yyyy-MM-dd") });
		return result;
	}
	requiredDateTimeOffset(dateTimeOffset: Date): Observable<object>  {
		const relativeUrl = `required-datetimeoffset`;
		const result = this.doGetAsync<object>(relativeUrl, { dateTimeOffset: format(dateTimeOffset, "yyyy-MM-ddTHH:mm:ssXXX") });
		return result;
	}
	requiredDateTimeOffsetDiffName(dateTimeOffset: Date): Observable<object>  {
		const relativeUrl = `required-datetimeoffset_diff-name`;
		const result = this.doGetAsync<object>(relativeUrl, { d: format(dateTimeOffset, "yyyy-MM-ddTHH:mm:ssXXX") });
		return result;
	}
	requiredEnumParameter(value: MyEnum): Observable<MyEnum>  {
		const relativeUrl = `required-enum-parameter`;
		const result = this.doGetAsync<MyEnum>(relativeUrl, { value });
		return result;
	}
	nullableString(name: string): Observable<object>  {
		const relativeUrl = `nullable-string`;
		const result = this.doGetAsync<object>(relativeUrl, { name });
		return result;
	}
	nullableStringImplied(name: string): Observable<object>  {
		const relativeUrl = `nullable-string-implied`;
		const result = this.doGetAsync<object>(relativeUrl, { name });
		return result;
	}
	nullableStringDiffName(name: string): Observable<object>  {
		const relativeUrl = `nullable-string-diff-name`;
		const result = this.doGetAsync<object>(relativeUrl, { n: name });
		return result;
	}
	nullableInt(value: number|undefined): Observable<object>  {
		const relativeUrl = `nullable-int`;
		const result = this.doGetAsync<object>(relativeUrl, { value });
		return result;
	}
	nullableDateTime(datetime: Date|undefined): Observable<object>  {
		const relativeUrl = `nullable-datetime`;
		const result = this.doGetAsync<object>(relativeUrl, { datetime });
		return result;
	}
	nullableDateTimeDiffName(datetime: Date|undefined): Observable<object>  {
		const relativeUrl = `nullable-datetime_diff-name`;
		const result = this.doGetAsync<object>(relativeUrl, { d: datetime });
		return result;
	}
	nullableDateOnly(dateonly: Date|undefined): Observable<object>  {
		const relativeUrl = `nullable-dateonly`;
		const result = this.doGetAsync<object>(relativeUrl, { dateonly });
		return result;
	}
	nullableDateOnlyDiffName(dateonly: Date|undefined): Observable<object>  {
		const relativeUrl = `nullable-dateonly_diff-name`;
		const result = this.doGetAsync<object>(relativeUrl, { d: dateonly });
		return result;
	}
	nullableDateTimeOffset(dateTimeOffset: Date|undefined): Observable<object>  {
		const relativeUrl = `nullable-datetimeoffset`;
		const result = this.doGetAsync<object>(relativeUrl, { dateTimeOffset });
		return result;
	}
	nullableDateTimeOffsetDiffName(dateTimeOffset: Date|undefined): Observable<object>  {
		const relativeUrl = `nullable-datetimeoffset_diff-name`;
		const result = this.doGetAsync<object>(relativeUrl, { d: dateTimeOffset });
		return result;
	}
	nullableEnumParameter(value: MyEnum|undefined): Observable<MyEnum>  {
		const relativeUrl = `nullable-enum-parameter`;
		const result = this.doGetAsync<MyEnum>(relativeUrl, { value });
		return result;
	}
}