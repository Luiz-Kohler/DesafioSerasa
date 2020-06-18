import config from '../settings/config.json';

export default class CompanyService {
  async getCompanies() {
    const ret = await fetch(
      config.endpoint + "/companies/get-order-by-descending"
    );
    return await ret.json();
  }

  async importFileCompany(values) {
    const data = new FormData();
    const imagedata = values.file;
    data.append('inputname', imagedata);
    fetch(config.endpoint + "/companies/"+values.idCompany, {
      method: "POST",
      body: data
    })
      .then(response => response.json())
      .then(success => console.log(success))
      .catch(error => console.log(error));
  }

  async save(values) {
    fetch(config.endpoint + "/companies", {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ ...values })
  }).then(response => response.json())
      .then(success => console.log(success))
      .catch(error => console.log(error));
  }
}
