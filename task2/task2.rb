require 'sha3'
files = Dir.entries(Dir.pwd).select {|f| !File.directory? f}
for file in files
    puts file + ' ' + SHA3::Digest.file(file).hexdigest
end