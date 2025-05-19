import axios from 'axios'

export abstract class BaseService {
  protected static axios = axios.create({
    baseURL: 'http://localhost:5121/api/v1/',
    headers: {
      common: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
    },
  })
}

