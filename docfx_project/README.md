# DocFX Documentation Project

This folder contains the DocFX documentation project for the Albatross CodeGen Framework.

## Generated vs Source Files

### Source Files (check into version control):
- `docfx.json` - DocFX configuration
- `index.md` - Homepage content
- `toc.yml` - Main table of contents
- `articles/` - All documentation articles
- `images/` - Documentation images and assets

### Generated Files (DO NOT check in):
- `api/` - Auto-generated API documentation YAML files
- `_site/` - Built documentation website

The `api/` folder is automatically generated from XML documentation comments in the source code and should be regenerated for each build.

## Building Documentation

### Prerequisites
```bash
# Install DocFX
dotnet tool install -g docfx
```

### Generate API Documentation
```bash
# From the docfx_project directory
docfx docfx.json --serve
```

This will:
1. Generate API metadata from the source projects
2. Build the complete documentation site
3. Serve it locally at http://localhost:8080

### Metadata Only
```bash
# Generate just the API metadata
docfx metadata docfx.json
```

### Build Only
```bash
# Build the site without serving
docfx build docfx.json
```

## CI/CD Integration

For automated documentation builds, only run the full DocFX build command. The API metadata will be generated automatically as part of the process.

Example GitHub Actions:
```yaml
- name: Build Documentation  
  run: |
    dotnet tool install -g docfx
    cd docfx_project
    docfx docfx.json
```