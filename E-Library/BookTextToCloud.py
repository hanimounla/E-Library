# import pip
# pip.main(['install', 'wordcloud'])

from wordcloud import WordCloud 
import matplotlib.pyplot as plt
import pypyodbc as db
from pandas import DataFrame
import sys


BookID = sys.args[1]
cnxn = db.connect('Driver={SQL Server};'
                                'Server=HANI\SQLSERVER2014;'
                                'Database=E-LibraryDB;'
                                'uid=hani Mounla;pwd=hanihani')
cnxn.cursor()
cursor = cnxn.cursor()

resoverall = cursor.execute("Select Text from data where bookID = " + BookID)
df = DataFrame(resoverall.fetchall())

#res = cursor.fetchall()
wordcloud = WordCloud(width = 1000, height = 500).generate(' '.join(df[0]))

plt.figure(figsize=(15,8))

plt.imshow(wordcloud)

plt.axis("off")

plt.show()

#plt.save('C:\Users\hani-_000\Desktop\\')
