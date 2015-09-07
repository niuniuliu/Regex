#/usr/local/bin/python
#encoding=utf-8

import re, os

file_name = __file__.split('/')[-1].replace('py', 'txt')

with open(file_name) as f:
    file_content = f.read()    
print file_content

pattern = r"#[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]"

print "===== %s ======" %pattern
pattern = re.compile(pattern)
result = re.findall(pattern, file_content)
print result

