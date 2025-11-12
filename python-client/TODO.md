1. deal with route encoding using quote. 
    deal with wild card routes

    ```python
    from urllib.parse import quote
    encoded_datetime = quote(datetime.isoformat(), safe='')
    relative_url = f"api/appointments/{encoded_datetime}"
    response = await self._client.get(relative_url)
    ```
2. add compression options