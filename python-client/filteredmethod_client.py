
class FilteredMethodClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def filtered_by_all():
		relativeUrl = f"all"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def filtered_by_none():
		relativeUrl = f"none"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def filtered_by_c_sharp():
		relativeUrl = f"csharp"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def filtered_by_c_sharp2():
		relativeUrl = f"csharp2"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def included_by_c_sharp():
		relativeUrl = f"include-this-method"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	
	AsyncModifier { Name = async } def filtered_by_type_script():
		relativeUrl = f"typescript"
		result = this.doGetAsync[None](relativeUrl, {})
		return await xx(result);
	

