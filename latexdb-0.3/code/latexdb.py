#!/usr/bin/env python
# -*- coding: iso-8859-15 -*-

#---------------------------------------------------------------------------
#  LatexDB 0.3  --  enhances LaTeX with database access features
#  Copyright (C) 2003-2006  Hans-Georg Eßer <h.g.esser@gmx.de>
#
#  Contributors:
#  Gerhard Kirchmann <gerhard.kirchmann@arcor.de>:
#    PostgreSQL code and code cleanup (using Cursors)
#  Francois Meyer <fmeyer@obs-besancon.fr>
#    Fix for a problem with recursion in the preparser
#
#  This program is free software; you can redistribute it and/or modify
#  it under the terms of the GNU General Public License as published by
#  the Free Software Foundation; either version 2 of the License, or
#  (at your option) any later version.
#
#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  GNU General Public License for more details.
#
#  You should have received a copy of the GNU General Public License
#  along with this program; if not, write to the Free Software
#  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
#---------------------------------------------------------------------------


# "Yes, I know, Python is object-oriented, but this is plain old
# procedural style, cause that's how I think of a program."  :)


"""
latexdb enhances standard latex by allowing you to use SQL queries
in your TeX documents.

There are three additional commands that you can embed in documents.
They are used for setting up the database connection (currently only
MySQL and PostgreSQL are supported), creating SQL select queries, and
accessing the result sets.

"""
from __future__ import generators
import re
global db




## debug
## Argument: String
## Function: Output debug code, or not...
global doDebug
doDebug=0
def debug(s):
    if doDebug: print "% //DEBUG: "+s
## debug ends



## texDBresult
## Generator function: needs Python 2.3 (or 2.2 with __future__)
## Argument: SQL query statement q
## Return value: One row of values
def texDBresult (q):
    # query database with q
    cursor=db.cursor()
    cursor.execute(q)

    while True:
        row=cursor.fetchone()
        if row==None:
            # empty row -- stop now
            cursor.close()
            break
        else:
            # new non-empty row
            yield row
## texDBresult ends




## texDBgetVarsFromString
## Argument: texDB enhanced LaTeX string with Variables
## Return value: List of ##xxx variables contained therein
def texDBgetVarsFromString(s):
    varPattern=r"(##[a-zA-Z0-9]+)"
    return re.findall(varPattern,s)
## texDBgetVarsFromString ends



## texDBgetQueryStringFromQuery
## Argument: Query name, e.g. q1
## Return value: SQL query string, e.g. SELECT * from ...
def texDBgetQueryStringFromQuery(q):
    retVal=0
    for item in _texDBqueries.items():
        if item[0][0]==q:
            retVal=item[1][0]
    return retVal
## texDBgetQueryStringFromQuery ends


## texDBconvert
## Argument: a value returned from the database
## Return value: String representation of value. In case of
##   a DateTime object, return val.date (2000-01-31) or
##   german version (31.1.2000)
def texDBconvert(val):
    # set locale to "de" for german dates, set it to anything other
    # for US (?) dates.
    locale="de"
    if str(type(val)) == "<type 'DateTime'>":
        if locale == "de":
            return str(val.day)+"."+str(val.month)+"."+str(val.year)
        else:
            return str(val.day)+" "+str(val.month)+" "+str(val.year)
    else:
        return str(val)
## texDBconvert ends


## texDBparseForLoop
## Argument: texDB string "\texdbfor{##query}{LaTeX stuff with ##Vars}
## Return value: LaTeX string without texDB stuff
def texDBparseForLoop(st):
    stdreg=re.compile(r"\\texdbfor{##([^}]*)}{(.*)}")
    m=stdreg.match(st)
    if not m:
        g=0
    else:
        g=m.groups()
    if not g:
        raise "texDB error: Invalid for loop"
    else:
        (qName,texString)=g
        debug ("% Query Name: "+qName)
        debug ("% TeX String: "+texString)

        # print "Variables:"
        # print texDBgetVarsFromString(texString)

        queryString=texDBgetQueryStringFromQuery(qName)
        # print queryString

        # now we query the database
        # and loop over the result set and do the substitutions
        for qR in texDBresult(queryString):
            # get variables used in this query
            substTexString=texString  # init
            for var in texDBgetVarsFromString(texString):
                qNamePlusVar=(qName,var)  # make tuple
                try:
                    varPosition=_texDBqueries[qNamePlusVar][1]
                    # the positional number is the 2nd argument (#1)
                    subs=texDBconvert(qR[varPosition])  # replace var with this
                    substTexString=re.sub(var,subs,substTexString)
                except:
                    # if ##var wasnt found, leave it untouched; go for
                    # another round of this script later
                    pass
            print substTexString
## texDBparseForLoop ends
           


## texDBparseDef
## Argument: texDB string "\texdbdef{##query}{SQL Query}
## Function: Enter new query in _texDBqueries
def texDBparseDef(st):
    stdreg=re.compile(r"\\texdbdef{##([^}]*)}{(.*)}{(.*)}")
    m=stdreg.match(st)
    if not m:
        g=0
    else:
        g=m.groups()
    if not g:
        raise "texDB error: Invalid def statement"
    else:
        (qName,qString,qVars)=g
        debug ("Query Name: "+qName)    # e.g. q1
        debug ("SQL Query:  "+qString)  # e.g. select Name from Users
        debug ("Query Vars: "+qVars)    # e.g. ##Name
        qVarsSplit = qVars.split(",")
        for i in range(0,len(qVarsSplit)):
            _texDBqueries[(qName,qVarsSplit[i])]=(qString,i)  # update dictionary
## texDBparseDef ends
        


### texDBparseDbDef
def texDBparseDbDef(st):
    global db
    stdreg=re.compile \
            (r"\\texdbconnection{([^,]*),([^,]*),([^,]*),([^,]*),([^}]*)}")
    m=stdreg.match(st)
    if not m:
        g=0
    else:
        g=m.groups()
    if not g:
        raise "texDB error: Invalid texdbconnection statement"
    else:
        (dbType,dbHost,dbUser,dbPass,dbDB)=g
        if dbType == "PostgreSQL":
            # from pyPgSQL import PgSQL ## pyPgSQL 2.4
            # db=PgSQL.connect(dbHost+':5432:'+dbDB+':'+dbUser+':'+dbPass)
            import psycopg ## psycopg 1.1.8
            db=psycopg.connect('host='+dbHost+' dbname='+dbDB+' user='+dbUser+' password='+dbPass)
        elif dbType == "MySQL":
            import MySQLdb
            db=MySQLdb.connect(dbHost,dbUser,dbPass,dbDB)
        else:
            raise "texDB error: only DB types MySQL and PostgreSQL supported right now."
    
### texDBparseDbDef ends



### texDBremovecomment
### removes all leading % characters from a string
def texDBremovecomment(s):
    #newstring=""
    #endofpercents=0
    #for ch in s:
    #    if ch!="%" or (ch=="%" and endofpercents): newstring+=ch
    #    if ch!="%": endofpercents=1
    #return newstring

    if s[0:8]=="%!texDB!":
        return s[8:]
    else:
        return s
### texDBremovecomment ends



### _main_()
### Arguments: stdin, stdout
### Function: preprocess LaTeX with texDB stuff in there
def _main_():
    from sys import stdin
    global _texDBqueries
    _texDBqueries={}   #  query dictionary, empty
    matchDef=re.compile(r"\\texdbdef{##([^}]*)}{(.*)}{(.*)}")
    matchFor=re.compile(r"\\texdbfor{##([^}]*)}{(.*)}")
    matchDbDef=re.compile \
                (r"\\texdbconnection{([^,]*),([^,]*),([^,]*),([^,]*),([^}]*)}")

    l=stdin.readline()
    while l:
        l=texDBremovecomment(l)
        if matchDef.match(l):
            # print "DEFINITION: ",l,
            texDBparseDef(l)
        elif matchFor.match(l):
            # print "FOR LOOP:   ",l,
            # print "%% texDB BEGIN FOR"
            texDBparseForLoop(l)
            # print "%% texDB END FOR"
        elif matchDbDef.match(l):
            texDBparseDbDef(l)
            print "%!texDB!"+l
        else:
            print l,   # just the line
        l=stdin.readline()
### _main_() ends



_main_()

