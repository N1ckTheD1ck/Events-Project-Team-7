from urllib.request import urlopen as uReq
from bs4 import BeautifulSoup as soup
import csv


my_url = 'https://www.thessalonikiguide.gr/events/sinavlies-live/'

#opening up connection,grabbing the page
uClient = uReq(my_url)
page_html = uClient.read()
uClient.close()

page_soup = soup(page_html, "html.parser")

#grabs all 
containers = page_soup.findAll("div",{"class":"entry-content"})

with open('postsnew.csv', 'w' , newline='' , encoding='utf-16') as f:
	fieldnames = ['post_name' , 'post_start_date' , 'post_end_date' , 'post_place' , 'post_address' , 'post_description']
	thewriter = csv.DictWriter(f,fieldnames=fieldnames)

	thewriter.writeheader()



	for container in containers:

		try:
			p_name = container.findAll("h3", {"class":"entry-title event-title"})
			post_name = p_name[0].a.text

			#post_image_src = container.div.a.img["data-lazy-src"]

			p_s_d = container.findAll("span", {"itemprop":"startDate"})
			post_start_date = p_s_d[0]["content"]

			p_e_d = container.findAll("span", {"itemprop":"endDate"})
			post_end_date = p_e_d[0]["content"]

			p_place = container.findAll("span", {"class":"event-place"})
			post_place = p_place[0].a.text

			p_add = container.findAll("span", {"class":"event-address"})
			post_address = p_add[0].span.text

			p_desc = container.findAll("div" , {"class":"jo-excerp-arch"})
			post_description = p_desc[0].text


		except:
			post_name = ""
			post_start_date = ""
			post_end_date = ""
			post_place = ""
			post_address = ""
			post_description = ""
	

		thewriter.writerow({'post_name' : post_name,'post_start_date' : post_start_date, 'post_end_date' : post_end_date, 'post_place' : post_place , 'post_address' : post_address , 'post_description' : post_description })





