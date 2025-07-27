import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios, { AxiosError, AxiosResponse } from "axios";

import Config from "@/config";
import { Bid } from "@/types/bid";
import Problem from "@/types/problem";
import { ApiResponse } from "@/types/apiResponse";

const useFetchBids = (id: number) => {
  return useQuery<ApiResponse<Bid[]>, AxiosError<Problem>>({
    queryKey: ["bids", id],
    queryFn: () =>
      axios
        .get(`${Config.baseApiUrl}/house/${id}/bids`)
        .then((resp) => resp.data),
  });
};

const useAddBid = () => {
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, Bid>({
    mutationFn: (x) =>
      axios
        .post(`${Config.baseApiUrl}/house/${x.houseId}/bids`, x),
    onSuccess: (_, bid) => {
      queryClient.invalidateQueries({ queryKey: ["bids", bid.houseId] });
    },
  });
};

export {
  useFetchBids,
  useAddBid
};
