
class CancellationTokenTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def get(cancellationToken: CancellationToken) -> str:
		relativeUrl = f""
		result = this.doGetStringAsync(relativeUrl, { "cancellationToken": cancellationToken })
		return await xx(result);
	

