#!/bin/bash

# Reset
Color_Off='\033[0m'       # Text Reset

# Regular Colors
Red='\033[0;31m'          # Red
Green='\033[0;32m'        # Green
Yellow='\033[0;33m'       # Yellow
Blue='\033[0;34m'         # Blue
Purple='\033[0;35m'       # Purple
Cyan='\033[0;36m'         # Cyan

####################################################
# variables holding commands
input="Enter commit message: "
add="git add -A"
# To be adding commit message later
commit="git commit -m "
push="git push"
pull="git pull"
####################################################

${pull}
status=`git status -s`
if [ -n "${status}" ]; then
	message=${1}
	# If the length of string is zero ==> EMPTY
	if [ -z "${message}" ]; then
		printf "${Purple}${input}${Cyan}"
		read message
		printf "${Color_Off}"
	fi
	${add}
	${commit} "${message}"
	${push}
fi

printf "Press enter key to quit..."
read -p ""
