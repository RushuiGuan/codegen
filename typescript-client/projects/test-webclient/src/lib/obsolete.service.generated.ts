import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ConfigService } from "@mirage/config";
import { WebClient } from "@mirage/webclient";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class ObsoleteService extends WebClient {
	get endPoint(): string  {
		return this.config.endpoint("test-client") + "api/obsolete";
	}
	constructor(private config: ConfigService, protected client: HttpClient) {
		super();
		console.log("ObsoleteService instance created");
	}
	get(): Observable<string>  {
		const relativeUrl = `get`;
		const result = this.doGetStringAsync(relativeUrl, {});
		return result;
	}
}
