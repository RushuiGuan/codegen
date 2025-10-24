
class PartiallyObsoleteClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def get() -> str:
		relativeUrl = f"get"
		result = this.doGetStringAsync(relativeUrl, {})
		return await xx(result);
	

