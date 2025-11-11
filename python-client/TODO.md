1. deal with route encoding using quote. 
    deal with wild card routes

    ```python
    from urllib.parse import quote
    encoded_datetime = quote(datetime.isoformat(), safe='')
    relative_url = f"api/appointments/{encoded_datetime}"
    response = await self._client.get(relative_url)
    ```

2. add compression options
3. if a datetime value has time zone, convert to utc
4. use 'Z' for the utc datetime value
    ```python
    text = value.astimezone(timezone.utc).isoformat().replace('+00:00', 'Z') if value.tzinfo else value.isoformat()
    ```


    value.astimezone(timezone.utc).isoformat().replace('+00:00', 'Z') if value.tzinfo not in (None, timezone.utc) else value.isoformat()