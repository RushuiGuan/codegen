
class ControllerRouteTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def post():
		relativeUrl = f""
		result = this.doPostAsync[None, str](relativeUrl, "", {})
		return await xx(result);
	

