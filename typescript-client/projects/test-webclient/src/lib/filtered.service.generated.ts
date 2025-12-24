import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ConfigService } from "@mirage/config";
import { WebClient } from "@mirage/webclient";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class FilteredService extends WebClient {
	get endPoint(): string  {
		return this.config.endpoint("test-client") + "api/filtered-controller";
	}
	constructor(private config: ConfigService, protected client: HttpClient) {
		super();
		console.log("FilteredService instance created");
	}
	thisShouldNoShowUp(): Observable<object>  {
		const relativeUrl = ``;
		const result = this.doGetAsync<object>(relativeUrl, {});
		return result;
	}
}