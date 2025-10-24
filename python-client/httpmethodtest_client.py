
class HttpMethodTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def delete():
		relativeUrl = f""
		result = this.doDeleteAsync(relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def post():
		relativeUrl = f""
		result = this.doPostAsync[None, str](relativeUrl, "", {})
		return await xx(result);
	
	AsyncModifier { Name = async } def patch():
		relativeUrl = f""
		result = this.doPatchAsync[None, str](relativeUrl, "", {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get() -> int:
		relativeUrl = f""
		result = this.doGetAsync[int](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def put():
		relativeUrl = f""
		result = this.doPutAsync[None, str](relativeUrl, "", {})
		return await xx(result);
	

