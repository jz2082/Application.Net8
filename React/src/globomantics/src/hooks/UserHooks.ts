import { useQuery } from "@tanstack/react-query";
import axios, { AxiosError } from "axios";

import Config from "@/config";
import { Claim } from "@/types/claim";
import Problem from "@/types/problem";
import { ApiResponse } from "@/types/apiResponse";

const useFetchUser = () => {
  console.log('useFetchUser');
  return useQuery<ApiResponse<Claim[]>, AxiosError<Problem>>({
    queryKey: ["user"],
    queryFn: () =>
      axios
        .get(`${Config.baseApiUrl}/api/user/list`)
        .then((resp) => resp.data),
  });
};
export default useFetchUser;
