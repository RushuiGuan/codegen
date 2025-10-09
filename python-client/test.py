from dataclasses import dataclass
from typing import Optional

@dataclass
class UserDto:
    id: int
    name: str
    email: str
    age: Optional[int] = None