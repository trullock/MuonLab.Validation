require 'bozo_scripts'

pre_compile :common_assembly_info do |c|
  c.company_name 'MuonLab'
end

# Creates a commonassemblyinfo.cs in ./build/ that all projects should reference

compile_with :msbuild

# will build all projects in ./{src|test}/csharp/

test_with :nunit do |n|
  n.project 'Trull.Common.Testing'
end

# will look in ./test/csharp/ for projects

package_with :nuget do |p|
  p.author 'Trull'
  p.license_url 'http://example.com'
  p.project_url 'http://example.com'
  p.library 'Trull.Common'
  p.library 'Trull.Common.Testing'
end

publish_with :file_copy do |fc|
  fc.directory 'dist', 'nuget'
  fc.destination '//YOUR/SHARE/'
end

post_publish :git_tag_release
# Will tag your repository when packages are published

resolve_dependencies_with :nuget do |n|
  n.source 'ADDITIONAL_SOURCE_URL_IF_YOU_NEED_IT'
end
# May not need this at all

with_hook :git_commit_hashes
# Embeds git hashes in commonassemblt info

with_hook :timing
# Basic timing stats for each section

build_tools_location '//WHERE/YOU/HAVE/TOOLS/NUGET.EXE'
# Needs to contain //WHERE/YOU/HAVE/TOOLS/nuget/NuGet.exe