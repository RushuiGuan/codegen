
class FromHeaderParamTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def omit_from_header_parameters():
		relativeUrl = f""
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	

