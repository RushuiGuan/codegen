# @generated
# type_adapter_cache.py
# Centralized lazy cache for pydantic TypeAdapter instances.
# Usage:
#   from type_adapter_cache import TypeAdapterCache
#   TypeAdapterCache.get(int).validate_python(response.json())
#   TypeAdapterCache.get(MyDto).dump_python(dto, mode="json")


from functools import lru_cache
from typing import Any
from pydantic import TypeAdapter

class TypeAdapterCache:
    """Centralized lazy cache for pydantic TypeAdapter instances."""

    @classmethod
    @lru_cache(maxsize=None)
    def get(cls, tp: Any) -> TypeAdapter:
        """Return a cached TypeAdapter for the given type."""
        return TypeAdapter(tp)
