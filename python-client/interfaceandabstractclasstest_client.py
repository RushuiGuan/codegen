
class InterfaceAndAbstractClassTestClient:
	endPoint: str
	__init__(self, base_url: str):
		self.base_url = base_url.rstrip("/")
	
	AsyncModifier { Name = async } def submit_by_interface(command: ICommand):
		relativeUrl = f"interface-as-param"
		result = this.doPostAsync[None, str](relativeUrl, "", { "command": command })
		return await xx(result);
	
	AsyncModifier { Name = async } def submit_by_abstract_class(command: AbstractClass):
		relativeUrl = f"abstract-class-as-param"
		result = this.doPostAsync[None, str](relativeUrl, "", { "command": command })
		return await xx(result);
	
	AsyncModifier { Name = async } def return_interface_async() -> ICommand:
		relativeUrl = f"return-interface-async"
		result = this.doPostAsync[ICommand, str](relativeUrl, "", {})
		return await xx(result);
	
	AsyncModifier { Name = async } def return_interface() -> ICommand:
		relativeUrl = f"return-interface"
		result = this.doPostAsync[ICommand, str](relativeUrl, "", {})
		return await xx(result);
	
	AsyncModifier { Name = async } def return_abstract_class_async() -> AbstractClass:
		relativeUrl = f"return-abstract-class-async"
		result = this.doPostAsync[AbstractClass, str](relativeUrl, "", {})
		return await xx(result);
	
	AsyncModifier { Name = async } def return_abstract_class() -> AbstractClass:
		relativeUrl = f"return-abstract-class"
		result = this.doPostAsync[AbstractClass, str](relativeUrl, "", {})
		return await xx(result);
	

