import sys
import csv
from io import StringIO
from faker import Faker
import re

def input_check():
    if len(sys.argv) > 3 or int(sys.argv[1]) < 1 or not re.match('[a-z]{2}_[A-Z]{2}', sys.argv[2]):
        print('Input format: first arg:{number of rows} second arg(region):{aa_AA}')
        exit(0)

def data_generation():
    columns = ["Name", "Address", 'Phone']
    fake = Faker(sys.argv[2])

    for _ in range(int(sys.argv[1])):
        data = {
            'Name': fake.name(),
            'Address': fake.address(),
            'Phone': fake.phone_number()
        }
        out = StringIO()
        csv.DictWriter(out, fieldnames=columns, delimiter=';', lineterminator = '\n').writerow(data)
        print(out.getvalue().replace('\n', ' '))


if __name__ == "__main__":
    input_check()
    data_generation()