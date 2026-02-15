import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ConfigService } from "@mirage/config";
import { WebClient } from "@mirage/webclient";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class FromHeaderParamTestService extends WebClient {
	get endPoint(): string  {
		return this.config.endpoint("test-client") + "api/from-header-param-test";
	}
	constructor(private config: ConfigService, protected client: HttpClient) {
		super();
		console.log("FromHeaderParamTestService instance created");
	}
	omitFromHeaderParameters(name: string): Observable<object>  {
		const relativeUrl = ``;
		const result = this.doGetAsync<object>(relativeUrl, {});
		return result;
	}
}