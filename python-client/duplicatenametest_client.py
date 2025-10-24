
class DuplicateNameTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def submit(id: int):
		relativeUrl = f"by-id"
		result = this.doPostAsync[None, str](relativeUrl, "", { "id": id })
		return await xx(result);
	
	AsyncModifier { Name = async } def submit1(name: str):
		relativeUrl = f"by-name"
		result = this.doPostAsync[None, str](relativeUrl, "", { "name": name })
		return await xx(result);
	

