
class RedirectTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def get():
		relativeUrl = f"test-0"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get1():
		relativeUrl = f"test-1"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get2():
		relativeUrl = f"test-2"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get3():
		relativeUrl = f"test-3"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get4():
		relativeUrl = f"test-4"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get5():
		relativeUrl = f"test-5"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get6():
		relativeUrl = f"test-6"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get7():
		relativeUrl = f"test-7"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get8():
		relativeUrl = f"test-8"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get9():
		relativeUrl = f"test-9"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def get10() -> str:
		relativeUrl = f"test-10"
		result = this.doGetStringAsync(relativeUrl, {})
		return await xx(result);
	

